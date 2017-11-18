import { ExistenceStatus } from '../enums/existence-status';

export class User {
  public Id: string;
  public FirstName: string;
  public LastName: string;
  public DisplayName: string;
  public Status: ExistenceStatus;
}
