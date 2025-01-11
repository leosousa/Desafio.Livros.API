import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssuntoListagemComponent } from './assunto-listagem.component';

describe('AssuntoListagemComponent', () => {
  let component: AssuntoListagemComponent;
  let fixture: ComponentFixture<AssuntoListagemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssuntoListagemComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AssuntoListagemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
