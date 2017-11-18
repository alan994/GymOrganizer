import { IntensityLevel } from '../enums/intensity-level';
import { Office } from './office';
import { ExistenceStatus } from '../enums/existence-status';
import { User } from './user';

export class Term {
  public Id: string;
  public Start: Date;
  public End: Date;
  public Capacity: number;
  public IntensityLevel: IntensityLevel;
  public Office: Office;
  public Status: ExistenceStatus;
  public Coach: User;
}
