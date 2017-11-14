import { HttpClient } from '@angular/common/http';
import { Office } from '../../models/web-api/office';
import { Injectable } from '@angular/core';

@Injectable()
export class OfficeService {
  constructor(private http: HttpClient) { }

  getAllActiveOffices() {
    return this.http.get<Office[]>('/api/offices');
  }
}
