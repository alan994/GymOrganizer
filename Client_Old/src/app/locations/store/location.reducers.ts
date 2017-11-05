import * as LocationActions from './location.actions';
import {Location} from '../../models/Location';

export interface State {
    locations: Location[];
}

const initialState: State = {
    locations: []
};

export function locationReducer(state: State = initialState, action: LocationActions.Actions){
    switch (action.type){
        case LocationActions.GET_ALL:
        return {
            ...state,
            locations: action.payload
        };
        default:
        return state;
    }
}