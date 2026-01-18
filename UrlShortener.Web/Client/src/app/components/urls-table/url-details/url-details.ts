import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {UrlDetailedData} from "@local/services";
import {DatePipe} from "@angular/common";
import {RouterLink} from "@angular/router";
import {environment} from "@local/env";

@Component({
    selector: 'url-details',
    standalone: true,
    templateUrl: './url-details.html',
    styleUrl: './url-details.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [
        DatePipe,
        RouterLink
    ]
})
export class UrlDetails {
    public readonly urlData = input.required<UrlDetailedData | undefined>();
    public readonly hostUrl = environment.hostUrl;
}
