import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { Term } from '../../models/web-api/term';
import { Observable } from 'rxjs/Observable';
import { Logger } from '../../services/utils/log.service';
import * as TermActions from '../store/term.actions';

@Component({
	selector: 'go-term-list',
	templateUrl: './term-list.component.html',
	styleUrls: ['./term-list.component.scss']
})
export class TermListComponent implements OnInit {

	terms: Observable<Term[]>;

	constructor(private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new TermActions.LoadGetTerms());
		this.terms = this.store.select(s => s.termReducer.terms);
	}

	deleteOffice(term: Term) {
		this.store.dispatch(new TermActions.DeleteTerm(term.id));
	}

}
