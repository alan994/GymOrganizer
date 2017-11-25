import { ExistenceStatus } from '../enums/existence-status';

export class User {
	public id: string;
	public firstName: string;
	public lastName: string;
	public email: string;
	public displayName: string;
	public status: ExistenceStatus;
	public active: boolean;
	public owed: number;
	public claimed: number;
	public isAdmin: boolean;
	public isCoach: boolean;
	public isGlobalAdmin: boolean;
	public tempPassword: string;
}
