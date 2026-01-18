import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {UrlDataService} from "@local/services";
import {FormsModule} from "@angular/forms";

@Component({
    selector: 'new-url',
    imports: [
        RouterLink,
        FormsModule
    ],
    templateUrl: './new-url.html',
    styleUrl: './new-url.scss',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewUrl {
    private readonly _urlDataService = inject(UrlDataService);
    private readonly _router = inject(Router);

    public url: string = "";

    public async onAddAsync(event: Event): Promise<void> {
        await this._urlDataService.addUrlEntryAsync(this.url);
        await this._router.navigate(['/']);
    }
}
