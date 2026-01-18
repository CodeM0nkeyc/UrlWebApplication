import {ActivatedRouteSnapshot, RouterStateSnapshot, GuardResult, MaybeAsync} from '@angular/router';
import {getCookie} from "@local/utilities";
import {environment} from "@local/env";

export function isAuthenticatedGuard(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
): MaybeAsync<GuardResult> {

    return getCookie(environment.authCookieIndicator) !== null;
}
