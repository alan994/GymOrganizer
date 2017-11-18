import { HttpClient } from '@angular/common/http';
import { Term } from '../../models/web-api/term';
import { Injectable } from '@angular/core';

@Injectable()
export class TermService {
  constructor(private http: HttpClient) { }

  getAllTerms() {
    return this.http.get<Term[]>('/api/terms');
  }

  getAllActiveTerms() {
    return this.http.get<Term[]>('/api/terms/active');
  }

  getTermById(id: string) {
    return this.http.get<Term>('/api/terms/' + id);
  }

  addTerm(term: Term) {
    return this.http.post('/api/terms', term);
  }

  editTerm(term: Term) {
    return this.http.put('/api/terms', term);
  }

  deleteTerm(id: string) {
    return this.http.delete('/api/terms/' + id);
  }
}
