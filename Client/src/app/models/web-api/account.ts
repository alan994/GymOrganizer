import { ApplicationRole } from '../enums/application-role';

export class Account {
	constructor(public firstName: string,
				public lastName: string,
				public displayName: string,
				public id: string,
				public externalId: string,
				public roles: ApplicationRole[]) {}
}
