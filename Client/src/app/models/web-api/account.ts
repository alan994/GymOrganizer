import { ApplicationRole } from '../enums/application-role';
import { User } from './user';
import { Tenant } from './tenant';

export class Account {
	public user: User;
	public tenant: Tenant;
}
