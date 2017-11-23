import { City } from './city';

export class Office {
	constructor(public id: string, public name: string, public address: string, public city: City, public active: boolean) { }
}
