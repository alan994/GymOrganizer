import { IntensityLevel } from '../enums/intensity-level';
import { Office } from './office';
import { ExistenceStatus } from '../enums/existence-status';
import { User } from './user';

export class Term {
	public id: string;
	public start: Date;
	public end: Date;
	public capacity: number;
	public intensityLevel: IntensityLevel;
	public office: Office;
	public status: ExistenceStatus;
	public active: boolean;
	public price: number;
	public coach: User;
}
