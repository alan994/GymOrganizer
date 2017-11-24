import { ExistenceStatus } from '../enums/existence-status';

export class Tenant {
	public id: string;
	public name: string;
	public settings: string;
	public configuration: TenantConfiguration;
	public status: ExistenceStatus;
	public active: boolean;
}

export class TenantConfiguration {

}
