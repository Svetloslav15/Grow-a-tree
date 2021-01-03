function getDifferenceBettweenDatesInMinutes(dt2, dt1) {
    let diff = (dt2.getTime() - dt1.getTime()) / 1000;
    diff /= 60;
    return Math.abs(Math.round(diff));
}

function getDifference(dateFuture, dateNow, isJson) {
    let diffInMilliSeconds = Math.abs(dateFuture - dateNow) / 1000;

    // calculate days
    const days = Math.floor(diffInMilliSeconds / 86400);
    diffInMilliSeconds -= days * 86400;

    // calculate hours
    const hours = Math.floor(diffInMilliSeconds / 3600) % 24;
    diffInMilliSeconds -= hours * 3600;

    // calculate minutes
    const minutes = Math.floor(diffInMilliSeconds / 60) % 60;
    diffInMilliSeconds -= minutes * 60;

    const seconds = Math.floor(diffInMilliSeconds % 60);

    let difference = '';
    if (days > 0) {
        difference += (days === 1) ? `${days} ден, ` : `${days} дни, `;
    }

    if (hours > 0) {
        difference += (hours === 1) ? `${hours} час, ` : `${hours} часове, `;
    }

    difference += (minutes === 0 || hours === 1) ? `${minutes} минута` : `${minutes} минути`;

    return isJson ? {
        days,
        hours,
        minutes,
        seconds
    } : difference;
}

export {getDifferenceBettweenDatesInMinutes, getDifference};