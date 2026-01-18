import {Routes} from "@angular/router";
import {UrlDetails} from "@local/components";
import {isAuthenticatedGuard, urlDetailedDataResolver} from "@local/utilities";
import {NewUrl} from "@local/components";

export const urlTableRoutes: Routes = [
    {
        path: "new",
        component: NewUrl,
        canActivate: [isAuthenticatedGuard]
    },
    {
        path: "",
        canActivate: [isAuthenticatedGuard],
        children: [
            {
                path: ":id",
                component: UrlDetails,
                resolve: { urlData: urlDetailedDataResolver }
            },
            {
                path: "index/:id",
                component: UrlDetails,
                resolve: { urlData: urlDetailedDataResolver }
            }
        ]
    },
];
