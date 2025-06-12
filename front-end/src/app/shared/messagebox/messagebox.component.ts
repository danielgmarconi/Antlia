import { Component } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { Subscription } from 'rxjs';
import { MessageboxService } from './messagebox.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-messagebox',
  imports: [CommonModule],
  templateUrl: './messagebox.component.html',
  styleUrl: './messagebox.component.scss'
})
export class MessageboxComponent {
  title: string = '';
  description: string = '';
  icon : any;
  isVisible = false;
  private modalSubscription!: Subscription
  constructor(private MessageboxService: MessageboxService, private sanitizer: DomSanitizer) {}

  ngOnInit() {
    this.modalSubscription = this.MessageboxService.modalState$.subscribe((state) => {
      this.isVisible = state.isVisible;
      this.title = state.title;
      this.description = state.description;
      this.icon =  this.sanitizer.bypassSecurityTrustHtml(state.icon);
    });

  }

  close() {
    this.MessageboxService.closeModal();
  }

  ngOnDestroy() {
    if (this.modalSubscription) {
      this.modalSubscription.unsubscribe();
    }
  }
}
