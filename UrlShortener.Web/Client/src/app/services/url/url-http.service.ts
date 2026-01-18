import {inject, Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "@local/env";
import {UrlBaseData, UrlDetailedData} from "./url-data.model";
import {lastValueFrom, map} from "rxjs";

@Injectable({
    providedIn: 'root',
})
export class UrlHttpService {
    private readonly _httpClient = inject(HttpClient);
    private readonly _apiUrl = environment.apiUrl + "/urls";

    public async getUrlsAsync(pageIndex: number): Promise<UrlBaseData[]> {
        const queryString = new HttpParams({ fromObject: { "pageIndex": pageIndex } });
        const responseData = this._httpClient.get<UrlBaseData[]>(`${this._apiUrl}`,
            { params: queryString });

        return await lastValueFrom(responseData);
    }

    public async getUrlAsync(id: number): Promise<UrlDetailedData> {
        const responseData = this._httpClient.get(`${this._apiUrl}/${id}`, { responseType: "text"})
            .pipe(map<string, UrlDetailedData>((value => {
                return JSON.parse(value, (key, value) => {
                    if (key === "createdAt") {
                        return new Date(value);
                    }

                    return value;
                })
        })));
        return await lastValueFrom<UrlDetailedData>(responseData);
    }

    public async postUrlAsync(url: string): Promise<UrlDetailedData | undefined> {
        return await lastValueFrom(
            this._httpClient.post<UrlDetailedData | undefined>(`${this._apiUrl}`, JSON.stringify(url), {
                headers: {
                    'Content-Type': 'application/json'
                }
            }));
    }

    public async deleteUrlAsync(id: number): Promise<void> {
        await lastValueFrom(this._httpClient.delete(`${this._apiUrl}/${id}`));
    }
}
