import * as NotificationActions from './notification.actions';

export interface State {
	processRequestIds: string[];
}

const initialState: State = {
	processRequestIds: []
};

export function notificationReducer(state: State = initialState, action: NotificationActions.NotificationActions) {
	switch (action.type) {
		case NotificationActions.NOTIFICATION_ADD:
			return {
				...state,
				processRequestIds: [...state.processRequestIds, action.payload]
			};
		case NotificationActions.NOTIFICATION_REMOVE:
			const newState = { ...state };
			const processRequestIdIndexToDelete = newState.processRequestIds.findIndex((value: string, index: number) => value === action.payload);
			newState.processRequestIds.splice(processRequestIdIndexToDelete, 1);
			return newState;

		default:
			return state;
	}
}
