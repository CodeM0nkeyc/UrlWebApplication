import {ActivatedRouteSnapshot, RouterStateSnapshot} from "@angular/router";
import {UrlDetailedData, UrlHttpService} from "@local/services";
import {inject} from "@angular/core";

export async function urlDetailedDataResolver(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
): Promise<UrlDetailedData | undefined> {

    const urlHttpService = inject(UrlHttpService);
    const urlId = Number(route.paramMap.get("id") ?? -1);
    const urlData = urlHttpService.getUrlAsync(urlId);

    if (!urlData) {
        return undefined;
    }

    return urlData;
}
