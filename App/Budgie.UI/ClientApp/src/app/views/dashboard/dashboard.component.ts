import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as moment from 'moment';

@Component({
  templateUrl: 'dashboard.component.html'
})
export class DashboardComponent implements OnInit {

  public brandPrimary = '#20a8d8';
  public brandSuccess = '#4dbd74';
  public brandInfo = '#63c2de';
  public brandWarning = '#f8cb00';
  public brandDanger = '#f86c6b';

  // mainChart

  public mainChartElements = 27;
  public mainChartData1: Array<number> = [];
  public mainChartData2: Array<number> = [];
  public mainChartData3: Array<number> = [];
  public mainChartData4: Array<number> = [];
  public mainChartData5: Array<number> = [];
  public mainChartData6: Array<number> = [];

  public mainChartData: Array<any> = [
    {
      type: 'bar',
      data: this.mainChartData1,
      label: 'Utilities'
    },
    {
      type: 'bar',
      data: this.mainChartData2,
      label: 'Groceries'
    },
    {
      type: 'bar',
      data: this.mainChartData3,
      label: 'Lunches'
    },
    {
      type: 'bar',
      data: this.mainChartData4,
      label: 'Eating out'
    },
    {
      data: this.mainChartData5,
      type: 'line',
      label: 'Savings'
    }
  ];
  /* tslint:disable:max-line-length */
  public mainChartLabels: Array<any> = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday', 'Monday', 'Thursday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  /* tslint:enable:max-line-length */
  public mainChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    scales: {
      xAxes: [{
        stacked: true,
        gridLines: {
          drawOnChartArea: false,
        },
        ticks: {
          callback: function (value: any) {
            return value.charAt(0);
          }
        }
      }],
      yAxes: [{
        stacked: true,
        ticks: {
          beginAtZero: true,
          maxTicksLimit: 5,
          stepSize: Math.ceil(500 / 5),
          max: 500
        }
      }]
    },
    elements: {
      line: {
        borderWidth: 2
      },
      point: {
        radius: 0,
        hitRadius: 10,
        hoverRadius: 4,
        hoverBorderWidth: 3,
      }
    },
    legend: {
      display: false
    }
  };

  public mainChartColours: Array<any> = [
    { // brandInfo
      backgroundColor: this.convertHex(this.brandPrimary, 10),
      borderColor: this.brandInfo,
      pointHoverBackgroundColor: '#fff'
    },
    { // brandSuccess
      backgroundColor: this.convertHex(this.brandSuccess, 10),
      borderColor: this.brandSuccess,
      pointHoverBackgroundColor: '#fff'
    },
    { // brandInfo
      backgroundColor: this.convertHex(this.brandDanger, 10),
      borderColor: this.brandDanger,
      pointHoverBackgroundColor: '#fff'
    },
    { // brandSuccess
      backgroundColor: this.convertHex(this.brandInfo, 10),
      borderColor: this.brandInfo,
      pointHoverBackgroundColor: '#fff'
    },
    { // brandSuccess
      backgroundColor: this.convertHex(this.brandWarning, 10),
      borderColor: this.brandWarning,
      pointHoverBackgroundColor: '#fff'
    },
    { // brandDanger
      backgroundColor: 'transparent',
      borderColor: this.brandDanger,
      pointHoverBackgroundColor: '#fff',
      borderWidth: 1,
      borderDash: [8, 5]
    }
  ];
  public mainChartLegend = false;
  public mainChartType = 'bar';

  // events
  public chartClicked(e: any): void {

  }

  public chartHovered(e: any): void {

  }

  // convert Hex to RGBA
  public convertHex(hex: string, opacity: number) {
    hex = hex.replace('#', '');
    const r = parseInt(hex.substring(0, 2), 16);
    const g = parseInt(hex.substring(2, 4), 16);
    const b = parseInt(hex.substring(4, 6), 16);

    const rgba = 'rgba(' + r + ', ' + g + ', ' + b + ', ' + opacity / 100 + ')';
    return rgba;
  }

  public random(min: number, max: number) {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  ngOnInit(): void {
    // generate random values for mainChart

    let cumulative: number = 0;

    for (let i = 0; i <= this.mainChartElements; i++) {
      this.mainChartData1.push(this.random(1, 100));
      this.mainChartData2.push(this.random(1, 100));
      this.mainChartData3.push(this.random(1, 100));
      this.mainChartData4.push(this.random(1, 100));
      this.mainChartData5.push(this.random(1, 100));
      cumulative += 1 * this.random(1, 10);
      this.mainChartData6.push(cumulative);
    }

    this.date = moment().format('MMMM YYYY');
  }

  radioModel: string = 'month';
  date: string = '';
}
