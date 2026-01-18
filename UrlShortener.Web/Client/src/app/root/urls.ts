import { Component } from '@angular/core';
import {ErrorMessage, UrlsTable} from "@local/components";

export * from "./urls.config";

@Component({
    selector: 'urls-root',
    standalone: true,
    templateUrl: './urls.html',
    imports: [
        UrlsTable,
        ErrorMessage
    ],
    styleUrl: './urls.scss'
})
export class Urls {

}
