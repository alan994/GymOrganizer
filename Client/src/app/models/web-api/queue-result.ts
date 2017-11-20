import { ProcessType } from '../enums/process-type';
import { Status } from '../enums/queue-result-status';
import { ExceptionCode } from '../enums/exception-code';

export class QueueResult {
	public userId: string;
	public tenantId: string;
	public status: Status;
	public requestOueueId: string;
	public exceptionCode?: ExceptionCode;
	public processType: ProcessType;
	public additionalData: Map<string, string>;
}