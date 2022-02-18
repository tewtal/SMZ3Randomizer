import { exposeWorker } from 'react-hooks-worker';

async function eventLoop(eventLoopHandle, randomizerClient) {
    await randomizerClient.update();
    eventLoopHandle = setTimeout(() => eventLoop(eventLoopHandle, randomizerClient), 1000);
}

exposeWorker(eventLoop);