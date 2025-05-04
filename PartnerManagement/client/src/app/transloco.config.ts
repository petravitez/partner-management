import { translocoConfig, TranslocoConfig } from '@ngneat/transloco';

export const translocoConfigObject: TranslocoConfig = translocoConfig({
  availableLangs: ['en', 'hr'],
  defaultLang: 'en',
  reRenderOnLangChange: true,
  prodMode: false
});
