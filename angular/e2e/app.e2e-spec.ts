import { WebCoreTemplatePage } from './app.po';

describe('WebCore App', function () {
    let page: WebCoreTemplatePage;

    beforeEach(() => {
        page = new WebCoreTemplatePage();
    });

    it('should display message saying app works', () => {
        page.navigateTo();
        expect(page.getParagraphText()).toEqual('app works!');
    });
});
