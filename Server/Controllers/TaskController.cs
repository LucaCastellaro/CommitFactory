using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CommitFactory.Persistence.DTO;
using CommitFactory.Services;
using System.Linq;

namespace CommitFactory.Controllers
{
    [ApiController]
    [EnableCors("Policy")]
    [Route("api/Mongo")]
    public class TaskController : ControllerBase
    {
        private readonly string _controllerName = "TaskController";
        private readonly ITasksService _tasksService;
        private readonly ILogService _logService;

        public TaskController(TasksService tasksService, LogService logService)
        {
            this._tasksService = tasksService;
            this._logService = logService;
        }

        #region [ GET ]

        [HttpGet("GetAllTasks")]
        public List<Task> GetAllTasks(DateTime? procedureTimestamp = null)
        {
            var log = new Log
            {
                ProcedureName = $"{this._controllerName}.GetAllTasks",
                ProcedureTimestamp = procedureTimestamp ?? DateTime.Now,
                Message = "Start"
            };

            this._logService.Log(log);

            var tasks = this._tasksService.GetAll()
                .OrderByDescending(i => i.Timestamp)
                .ToList();

            log.Message = $"Found {tasks.Count} document(s)";
            log.Payload = tasks.ToList<object>();
            this._logService.Log(log);

            log.Message = "End";
            this._logService.Log(log);

            return tasks;
        }

        [HttpGet("GetLimitedNumberOfTasks")]
        public List<Task> GetLimitedNumberOfTasks(int tasksToGet, int tasksToSkip)
        {
            var log = new Log
            {
                ProcedureName = $"{this._controllerName}.GetLimitedNumberOfTasks",
                ProcedureTimestamp = DateTime.Now,
                Message = "Start"
            };

            this._logService.Log(log);

            var tasks = this.GetAllTasks(log.ProcedureTimestamp)
                .Skip(tasksToSkip)
                .Take(tasksToGet)
                .ToList();

            log.Message = $"Skip first {tasksToSkip} document(s) and then take {tasks.Count} document(s)";
            log.Payload = tasks.ToList<object>();
            this._logService.Log(log);

            log.Message = "End";
            this._logService.Log(log);

            return tasks;
        }

        #endregion [ GET ]

        #region [ POST ]

        [HttpPost("AddTask")]
        public bool AddTask(Task task)
        {
            var log = new Log
            {
                ProcedureName = $"{this._controllerName}.AddTask",
                ProcedureTimestamp = DateTime.Now,
                Message = "Start"
            };

            this._logService.Log(log);

            var result = false;

            try
            {
                log.Message = $"Trying to insert 1 new task";
                log.Payload = new List<object>() { task };
                this._logService.Log(log);

                this._tasksService.InsertOne(task);

                result = true;
            }
            catch (Exception ex)
            {
                log.Exception = ex.Message;
                this._logService.Error(log);

                result = false;
            }
            finally
            {
                log.Message = "End";
                this._logService.Log(log);
            }

            return result;
        }

        #endregion [ POST ]

        #region [ DELETE ]

        [HttpDelete("DeleteTask")]
        public bool DeleteTask(string jsonTask)
        {
            var log = new Log
            {
                ProcedureName = $"{this._controllerName}.DeleteTask",
                ProcedureTimestamp = DateTime.Now,
                Message = "Start"
            };

            this._logService.Log(log);

            var result = false;

            var task = JsonConvert.DeserializeObject<Task>(jsonTask);
            try
            {
                log.Message = $"Trying to delete 1 task";
                log.Payload = new List<object>() { task };
                this._logService.Log(log);
                this._tasksService.DeleteOne(task);

                result = true;
            }
            catch (Exception ex)
            {
                log.Exception = ex.Message;
                log.Payload = new List<object>() { task };
                this._logService.Error(log);

                result = false;
            }
            finally
            {
                log.Message = "End";
                this._logService.Log(log);
            }

            return result;
        }

        [HttpDelete("DeleteOldTasks")]
        public bool DeleteOldTasks()
        {
            var log = new Log
            {
                ProcedureName = $"{this._controllerName}.DeleteOldTasks",
                ProcedureTimestamp = DateTime.Now,
                Message = "Start"
            };

            this._logService.Log(log);

            var result = false;

            try
            {
                var date = log.ProcedureTimestamp.Date;
                var tasks = this._tasksService.CountOldTasks(date);

                log.Message = $"Found {tasks} document(s) with date < {date}";
                this._logService.Log(log);

                if (tasks > 0)
                {
                    this._tasksService.DeleteOldTasks(date);
                    log.Message = $"Deleted {tasks} document(s)";
                }

                this._logService.Log(log);

                result = true;
            }
            catch (Exception ex)
            {
                log.Exception = ex.Message;
                this._logService.Error(log);

                result = false;
            }
            finally
            {
                log.Message = "End";
                this._logService.Log(log);
            }

            return result;
        }

        #endregion [ DELETE ]

    }
}