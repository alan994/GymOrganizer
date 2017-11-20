import { Injectable } from '@angular/core';

import { Store } from '@ngrx/store';
import * as _ from 'lodash';
import { HubConnection } from '@aspnet/signalr-client';

import { Logger } from './log.service';
import * as fromApp from '../../store/app.reducers';
import * as NotificationActions from '../../store/notifications/notification.actions';
import { QueueResult } from '../../models/web-api/queue-result';
import { NotificationHelperService } from './notification-helper.service';

@Injectable()
export class SignalRService {

	private hubConnection: HubConnection;

	constructor(private logger: Logger, private store: Store<fromApp.AppState>, private notificationHelper: NotificationHelperService) {
		this.hubConnection = new HubConnection('http://localhost:5002/notifications');

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
				this.logger.error('Error while establishing connection with server', err);
			});
	}

	private registerOnServerEvents(): void {
		this.hubConnection.on('notificationArrived', (data: QueueResult) => {
			this.logger.info('SingalR notification arrived: ', data);
			this.notificationHelper.handleQueueResponse(data);
		});
	}
}
