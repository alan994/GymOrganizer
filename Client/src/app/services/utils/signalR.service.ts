import { Injectable } from '@angular/core';

import { Store } from '@ngrx/store';
import * as _ from 'lodash';
import { HubConnection } from '@aspnet/signalr-client';

import { Logger } from './log.service';
import * as fromApp from '../../store/app.reducers';
import * as NotificationActions from '../../store/notifications/notification.actions';

@Injectable()
export class SignalRService {

	private hubConnection: HubConnection;

	constructor(private logger: Logger, private store: Store<fromApp.AppState>) {
		this.hubConnection = new HubConnection('http://localhost:5000/notifications');

		this.registerOnServerEvents();
		this.startConnection();
	}

	init() {

	}

	private startConnection(): void {
		this.hubConnection.start()
			.then(() => {
				this.logger.debug('Hub connection started');
			})
			.catch(err => {
				this.logger.error('Error while establishing connection with server');
			});
	}

	private registerOnServerEvents(): void {
		this.hubConnection.on('notificationArrived', (data: string) => {
			this.store.select(s => s.notificationState.processRequestIds)
				.take(1)
				.map((processRequestIds: string[]) => {
					return _.first(_.filter(processRequestIds, x => x === data));
				})
				.subscribe((processRequestId: string) => {
					if (processRequestId) {
						console.log(`Ugasi notifikaciju s id-om ${processRequestId}`);
						this.store.dispatch(new NotificationActions.RemoveNotification(processRequestId));
						this.logger.success('Operacija je završena');
						// this.store.dispatch(new BookActions.LoadGetAllBooks());
					}
				});
		});
	}
}
