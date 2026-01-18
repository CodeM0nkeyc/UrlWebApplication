import { Injectable } from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {HttpError} from "./http-error.model";

@Injectable({
  providedIn: 'root',
})
export class HttpErrorService {
    private errorSubject = new BehaviorSubject<HttpError | null>(null);

    public get errors$() {
        return this.errorSubject.asObservable();
    }

    pushError(error: HttpError) {
        this.errorSubject.next(error);
    }

}
