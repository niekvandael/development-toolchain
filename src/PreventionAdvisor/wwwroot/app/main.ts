// main entry point
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { AppModule } from './app.module';
import { enableProdMode } from '@angular/core';

if (location.protocol === 'https:') {
    enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule);
