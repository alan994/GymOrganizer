import { Injectable } from '@angular/core';


@Injectable()
export class Logger {
	constructor() {
	}

	info(msg: string, ...data: any[]) {
		console.info(msg, data);
	}

	debug(msg: string, ...data: any[]) {
		console.log(msg, data);
	}

	success(msg: string, ...data: any[]) {
		console.info(msg, data);
	}

	warning(msg: string, ...data: any[]) {
		console.warn(msg, data);
	}

	error(msg: string, ...data: any[]) {
		console.error(msg, data);
	}
}
