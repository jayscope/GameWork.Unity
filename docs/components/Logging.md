# Overview

Unity specifc implementation for [GameWork.Core Logging](https://github.com/JaredGG/GameWork.Core/blob/master/docs/components/Logging.md).

# Usage
It is recommended to use the ThreadedLogger to avoid blocking the main thread while writing logs. This just wraps up the standard Unity Logger to write the logs. 

# Gotchas
WebGL doesn't support Thread creation so the ThreadedLogger does not currently support WebGL.  
An easy workaround is to use a standard logger if `Application.platform == RuntimePlatform.WebGLPlayer`.