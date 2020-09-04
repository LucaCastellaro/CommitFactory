import { LogLevel } from '../enums/log-level.enum';

export class Log {
    public LogLevel: LogLevel;
    public Timestamp: Date;
    public ProcedureTimestamp: Date;
    public ProcedureName: string;
    public Message: string;
    public Payload: string;
    public Exception: string;
}