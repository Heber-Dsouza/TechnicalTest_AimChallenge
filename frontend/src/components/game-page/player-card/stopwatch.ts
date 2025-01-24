class CountdownTimer {
  private remainingTime: number;
  private intervalId: NodeJS.Timeout | null = null;

  constructor(initialTime: number) {
    this.remainingTime = initialTime;
  }

  start() {
    //console.log(`Timer iniciado: ${this.remainingTime} ms`);

    this.intervalId = setInterval(() => {
      this.remainingTime -= 1000;

      if (this.remainingTime <= 0) {
        // console.log("Timer finalizado!");
        this.stop();
      } 
      // else {
      //   console.log(`Tempo restante: ${this.remainingTime} ms`);
      // }
    }, 1000);
  }

  stop() {
    if (this.intervalId) {
      clearInterval(this.intervalId);
      this.intervalId = null;
    }
    this.remainingTime = Math.max(this.remainingTime, 0);
    // console.log("Timer parado.");
  }
}
