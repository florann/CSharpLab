import { Component, inject, OnInit} from '@angular/core';
import { IdleService } from '../../core/services/idle/idle';

@Component({
  selector: 'app-footer',
  imports: [],
  templateUrl: './footer.html',
  styleUrl: './footer.scss',
})

export class Footer implements OnInit {
  
  idleService = inject(IdleService);

  ngOnInit(): void {
    this.idleService.startWatching();  
  }
}
