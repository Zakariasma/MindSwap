import { TestBed } from '@angular/core/testing';

import { CardUploadService } from './card-upload.service';

describe('CardUploadService', () => {
  let service: CardUploadService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CardUploadService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
