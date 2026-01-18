import {HttpErrorResponse, HttpInterceptorFn} from '@angular/common/http';
import {catchError, throwError} from "rxjs";
import {inject} from "@angular/core";
import {HttpErrorService, HttpError} from "@local/services";

export const httpErrorInterceptor: HttpInterceptorFn = (req, next) => {
    const errorService = inject(HttpErrorService);

    return next(req).pipe(
        catchError((error: HttpErrorResponse) => {
            if (error.error) {
                if (error.status >= 500) {
                    errorService.pushError({
                        code: "Internal",
                        description: "Something went wrong. Please try again later."
                    });
                }
                else if (error.status === 0) {
                    errorService.pushError({
                        code: "Offline",
                        description: "No connection to the server."
                    });
                }
                else {
                    const err = (error.error as HttpError[]).pop();

                    if (err) {
                        errorService.pushError(err);
                    }
                }
            }
            return throwError(() => error);
        })
    );
};
