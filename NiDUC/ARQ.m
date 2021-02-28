n = 4672;

signal = generate(n);
parity = isParity(signal);
frames = enframe(signal, 7);
encodedFrames = frameEncode(frames, 7);