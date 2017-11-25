import { Component, OnInit } from '@angular/core';
import { IntensityLevel } from '../../models/enums/intensity-level';
import { Observable } from 'rxjs/Observable';
import { Term } from '../../models/web-api/term';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import * as TermActions from '../store/term.actions';
import * as UserActions from '../../user/store/user.actions';
import * as _ from 'lodash';
import { User } from '../../models/web-api/user';
import { Office } from '../../models/web-api/office';
import * as OfficeActions from '../../office/store/office.actions';

@Component({
	selector: 'go-term-edit',
	templateUrl: './term-edit.component.html',
	styleUrls: ['./term-edit.component.scss']
})
export class TermEditComponent implements OnInit {
	termId: string;
	coaches: Observable<User[]>;
	offices: Observable<Office[]>;
	termForm: FormGroup;
	isEdit: boolean;
	public intensityLevels = [
		{
			id: IntensityLevel.Beginner,
			text: 'Beginer'
		},
		{
			id: IntensityLevel.Normal,
			text: 'Normal'
		},
		{
			id: IntensityLevel.Pro,
			text: 'Pro'
		}
	];

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger
	) {
		this.store.dispatch(new UserActions.LoadGetUsers());
		this.store.dispatch(new OfficeActions.LoadGetOffices());


		this.termForm = this.formBuilder.group(
			{
				id: [null],
				start: [new Date(), Validators.required],
				end: [new Date(), Validators.required],
				intensityLevel: [IntensityLevel.Beginner, Validators.required],
				capacity: [null, [Validators.required, Validators.min(1)]],
				price: [null, [Validators.required, Validators.min(0)]],
				coach: [null, [Validators.required]],
				office: [null, [Validators.required]],
				active: true
			}
		);
		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.termId = params.termId; this.update(); });
	}

	ngOnInit() {
		this.coaches = this.store.select(s => s.userReducer.users);
		this.offices = this.store.select(s => s.officeReducer.offices);
	}

	update() {
		if (this.termId) {
			this.store.select(x => x.termReducer.terms)
				.take(1)
				.map((terms: Term[]) => {
					return _.find(terms, o => o.id === this.termId);
				})
				.subscribe((term: Term) => {
					this.logger.debug('Term: ', term);
					//Handle cities
					this.termForm.patchValue({
						id: this.termId,
						start: term.start,
						end: term.end,
						intensityLevel: term.intensityLevel,
						capacity: term.capacity,
						price: term.price,
						office: term.office,
						coach: term.coach,
						active: term.active
					});
				});
		}
	}

	add() {
		this.store.dispatch(new TermActions.AddTerm(this.termForm.value));
		this.logger.info('Add: ', this.termForm.value, this.termForm);
	}

	edit() {
		this.store.dispatch(new TermActions.EditTerm(this.termForm.value));
		this.logger.info('Edit: ', this.termForm.value, this.termForm);
	}
}
