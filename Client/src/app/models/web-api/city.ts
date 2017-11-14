import { Country } from './country';

export class City {
  constructor(public id: string, public name: string, public postalCode: string, public country: Country) { }
}
