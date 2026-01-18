import {inject, Injectable, signal} from '@angular/core';
import {UrlHttpService} from "./url-http.service";
import {UrlBaseData} from "./url-data.model";

@Injectable({
    providedIn: 'root',
})
export class UrlDataService {
    private readonly _urlHttpService = inject(UrlHttpService);
    private readonly _urls = signal<UrlBaseData[]>([]);

    public get urls() {
        return this._urls.asReadonly();
    }

    public async loadUrlEntriesAsync(pageIndex: number): Promise<void> {
        const urls = await this._urlHttpService.getUrlsAsync(pageIndex);
        this._urls.update(oldValue => [...oldValue, ...urls]);
    }

    public async addUrlEntryAsync(url: string): Promise<void> {
        const urlData = await this._urlHttpService.postUrlAsync(url);

        if (urlData) {
            this._urls.update(oldValue => [...oldValue, urlData]);
        }
    }

    public async removeUrlEntryAsync(urlId: number): Promise<void> {
        await this._urlHttpService.deleteUrlAsync(urlId);
        this._urls.update(oldValue => [...oldValue.filter(x => x.id !== urlId)]);
    }
}
