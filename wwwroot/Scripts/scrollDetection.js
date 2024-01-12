function initializePostViewDetection(dotnetReference, postId, minimumViewTime) {
    const postElement = document.getElementById(postId);

    let timer, startTime;
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                // Post is in view, start the timer
                startTime = Date.now();
                timer = setTimeout(() => {
                    let viewTimeInSeconds = Math.floor((Date.now() - startTime) / 1000);
                    dotnetReference.invokeMethodAsync('OnPostViewed', postId, viewTimeInSeconds);
                }, minimumViewTime);
            } else {
                // Post is not in view, clear the timer
                clearTimeout(timer);
            }
        });
    }, { threshold: 0.5 }); // threshold can be adjusted

    observer.observe(postElement);

    return {
        dispose: () => {
            observer.unobserve(postElement);
            clearTimeout(timer);
        }
    };
}
