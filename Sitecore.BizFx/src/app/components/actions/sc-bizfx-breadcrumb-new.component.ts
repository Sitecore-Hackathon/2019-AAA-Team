import { Component, Input, Output, EventEmitter, ViewChild, OnInit, AfterViewInit, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { ScDialogService } from '@speak/ng-bcl/dialog';
import { ScAction } from '@speak/ng-bcl/action-control';

import { ScBizFxAction, ScBizFxView } from '@sitecore/bizfx';
import { ScBizFxActionComponent } from './sc-bizfx-action.component';
import { ScBizFxContextService, ScBizFxViewsService } from '@sitecore/bizfx';

/**
 * BizFx View Action Bar `Component`.
 *
 * Reders an action bar for a view.
 */
@Component({
  selector: 'sc-bizfx-breadcrumb-new',
  templateUrl: './sc-bizfx-breadcrumb-new.component.html',
  styleUrls: ['./sc-bizfx-breadcrumb-new.component.css'],
  host: {
    '(document:click)': 'onClick($event)',
  }
})



/**
 * BizFx View Action Bar `Component`.
 */
export class ScBizFxBreadcrumbNewComponent implements AfterViewInit { 
  sub: any;
  currentRoute: string;

  constructor(
    protected http: HttpClient,
    private router: Router,
    private _eref: ElementRef) {
      router.events.subscribe(val => {
        var newVar = val as any
        console.log(val );
        if(newVar.url.indexOf('Entity-Category') >= 0 || newVar.url.indexOf('Entity-SellableItem') >= 0 || newVar.url.indexOf('Entity-Catalog') >= 0 ) {
          this.display= true;
        } else {
          this.display = false;
        }    
      })
    }
    public display = true;
    public breadCrumbs;

  ngOnInit() {
    /*this.sub = this.route
    .data
    .subscribe(v => console.log(v));*/
    if(true) {return null;}
  }
  
  @Input() view: ScBizFxView;
  private apiUrl = 'api/breadcrumbs';
  
  onClick(event) {
    if (!this._eref.nativeElement.contains(event.target)){ // or some similar check
      this.breadCrumbs.map((item, index) => (
        item.active = false
      ))
    }
  }

  ngAfterViewInit(): void {
    if (window.location.href.indexOf('entityView') < 0) { this.display = false; }
    const url = this.apiUrl;
    debugger
    this.breadCrumbs = [
      {
        title: 'test',
        url: 'est',
        id: '123'
      },
      {
        title: 'test',
        url: 'est',
        id: '234'
      },
      {
        title: 'test',
        url: 'est',
        id: '345'
      }
    ]
    this.http.get(url)
    .subscribe(
      response => {
        console.log('API answer: ', response);
     
      },
      err => {
        console.log('Server error: ' + JSON.stringify(err));
        
      }
    )
     
   // if (this.view === undefined) { return; }
  }
  loadSubItems(event, itemId): void {
    var currentIndex = this.breadCrumbs.findIndex(item => item.id == itemId.id);

    this.breadCrumbs.forEach(element => {
      element.active = false ;
    });

    if(!itemId.children) {
      new Promise ((resolve, reject) => {
        const url = this.apiUrl;
        
        this.http.get(url + '?' + itemId.id)
        .subscribe(
          response => {
            console.log('API answer: ', response);
           
            this.breadCrumbs[currentIndex].children = [
              {
                title: 'test',
                url: 'est',
                id: '123',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              },
              {
                title: 'test',
                url: 'est',
                id: '234',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              },
              {
                title: 'test',
                url: 'est',
                id: '345',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              }
            ]
          },
          err => {
            console.log('Server error: ' + JSON.stringify(err));
            this.breadCrumbs[currentIndex].children = [
              {
                title: 'test',
                url: 'est',
                id: '123',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              },
              {
                title: 'test',
                url: 'est',
                id: '234',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              },
              {
                title: 'test',
                url: 'est',
                id: '345',
                img: 'https://image.flaticon.com/icons/svg/25/25694.svg'
              }
            ]
          }
        );
      });

      this.breadCrumbs[currentIndex].active = !this.breadCrumbs[currentIndex].active;

    } else {
      this.breadCrumbs[currentIndex].active = !this.breadCrumbs[currentIndex].active;

    }
    
  }
  checkItemExists(item): boolean {
    return typeof(item.children) != "undefined"
  }
}/*
  /**
     * Defines the view to be render
     */