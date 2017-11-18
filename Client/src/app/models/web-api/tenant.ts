import { ExistenceStatus } from '../enums/existence-status';

export class Tenant {
  public Id: string;
  public Name: string;
  public Settings: string;
  public Configuration: TenantConfiguration;
  public Status: ExistenceStatus;
}

export class TenantConfiguration {

}
