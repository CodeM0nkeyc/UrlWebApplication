import {ChangeDetectionStrategy, Component, DestroyRef, inject, signal} from '@angular/core';
import {skip} from "rxjs";
import {HttpErrorService} from "@local/services";

@Component({
    selector: 'error-message',
    standalone: true,
    templateUrl: './error-message.html',
    styleUrl: './error-message.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ErrorMessage {
    private readonly _destroyRef = inject(DestroyRef);

    private readonly _httpErrorService = inject(HttpErrorService);

    public readonly error = signal<string | null>(null);
    public readonly opened = signal<boolean>(false);

    public constructor() {
        const subscription = this._httpErrorService.errors$
            .pipe(skip(1))
            .subscribe(message => {
                if (message && message.description !== "") {
                    this.opened.set(true);
                    this.error.set(message.description);
                }
            });

        this._destroyRef.onDestroy(() => {
            subscription.unsubscribe();
        });
    }

    public onClose(event: Event): void {
        this.opened.set(false);
    }
}
