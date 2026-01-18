import {ChangeDetectionStrategy, Component, inject, Signal} from '@angular/core';
import {UrlBaseData, UrlDataService} from "@local/services";
import {RouterLink, RouterOutlet} from "@angular/router";
import {getCookie} from "@local/utilities";
import {environment} from "@local/env";

@Component({
    selector: 'urls-table',
    standalone: true,
    templateUrl: './urls-table.html',
    styleUrl: './urls-table.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        RouterOutlet,
        RouterLink
    ]
})
export class UrlsTable {
    private readonly _urlDataService = inject(UrlDataService);

    private _pageIndex = 1;

    public readonly isAuthenticated = getCookie(environment.authCookieIndicator) !== null;
    public readonly hostUrl = environment.hostUrl;

    public readonly urlsData: Signal<UrlBaseData[]>;

    public constructor() {
        this.urlsData = this._urlDataService.urls;
        this._urlDataService.loadUrlEntriesAsync(this._pageIndex++);
    }

    public async onRemoveAsync(event: Event): Promise<void> {
        const target = event.target as HTMLElement;
        let urlDataId = target.dataset["urlId"];

        if (!urlDataId) {
            return;
        }

        const urlId = Number(urlDataId);

        if (isNaN(urlId) || urlId < 1) {
            console.error("Incorrect URL Id: " + urlId);
            return;
        }

        await this._urlDataService.removeUrlEntryAsync(urlId);
    }
}
