import { ApplicationConfig, provideBrowserGlobalErrorListeners } from '@angular/core';
import {provideRouter, withComponentInputBinding} from "@angular/router";
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {routes} from "./urls.routes";
import {httpErrorInterceptor} from "@local/utilities";

export const urlsConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes, withComponentInputBinding()),
        provideHttpClient(withInterceptors([httpErrorInterceptor]))
    ]
};
