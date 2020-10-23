using System;
using System.Collections.Generic;
using System.Linq;
using CommitFactory.Persistence.DTO;
using CommitFactory.Persistence.Models;
using CommitFactory.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[EnableCors("Policy")]
[Route("api/Log")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;

    public LogController(ILogService logService)
    {
        this._logService = logService;
    }

    [HttpGet("GetAllLogs")]
    public List<MongoLog> GetAllLogs(DateTime? procedureTimestamp = null)
    {
        var log = new Log
        {
            ProcedureName = $"{this.GetType().Name}.GetAllLogs",
            ProcedureTimestamp = procedureTimestamp ?? DateTime.Now,
            Message = "Start"
        };

        this._logService.Log(log);

        var logs = this._logService.GetAll()
            .OrderByDescending(i => i.ProcedureTimestamp)
            .ThenByDescending(i => i.Timestamp)
            .ToList();

        log.Message = $"Found {logs.Count} document(s)";
        log.Payload = logs
            .Select(log => new LogWithoutPayload
            {
                Exception = log.Exception,
                Message = log.Message,
                ProcedureName = log.ProcedureName,
                ProcedureTimestamp = log.ProcedureTimestamp
            })
            .ToList<object>();
        this._logService.Log(log);

        log.Message = "End";
        this._logService.Log(log);

        return logs;
    }

    [HttpGet("GetLimitedNumberOfLogs")]
    public List<MongoLog> GetLimitedNumberOfLogs(int logsToGet, int logsToSkip)
    {
        var log = new Log
        {
            ProcedureName = $"{this.GetType().Name}.GetLimitedNumberOfLogs",
            ProcedureTimestamp = DateTime.Now,
            Message = "Start"
        };

        this._logService.Log(log);

        var logs = this.GetAllLogs(log.ProcedureTimestamp)
            .Skip(logsToSkip)
            .Take(logsToGet)
            .ToList();

        log.Message = $"Skip first {logsToSkip} document(s) and then take {logs.Count} document(s)";
        log.Payload = logs
            .Select(log => new LogWithoutPayload
            {
                Exception = log.Exception,
                Message = log.Message,
                ProcedureName = log.ProcedureName,
                ProcedureTimestamp = log.ProcedureTimestamp
            })
            .ToList<object>();

        this._logService.Log(log);

        log.Message = "End";
        this._logService.Log(log);

        return logs;
    }
}