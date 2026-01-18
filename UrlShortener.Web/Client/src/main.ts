import { bootstrapApplication } from '@angular/platform-browser';
import { urlsConfig } from '@local/root';
import { Urls } from '@local/root';

bootstrapApplication(Urls, urlsConfig)
  .catch((err) => console.error(err));
