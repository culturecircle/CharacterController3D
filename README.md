### 摄像机的旋转

1. 摄像机围绕主角旋转，依据鼠标在屏幕上的偏移量来计算旋转角度。
2. 在屏幕两侧的边界虚化处理，借助的是外部函数（相对Unity而言），依据Windows DX的惯性，这个外部函数的坐标系和Unity内部的屏幕坐标系原点位置不同，在纵坐标上需要翻转计算。
3. 同样是两侧边界的虚化问题，屏幕虽说是1920*1080，但是在实际使用时并不会达到完全准确的1920（以Width为例），实际情况是最大值只能达到1918左右。在边界采用范围检测而不是达到某一边界值进行鼠标位置设置。

### 主角运动、朝向以及动画

1. WASD对角色的控制为朝向改变，转向的策略以摄像机的局部坐标系为参照，W朝向与摄像机的forward一致，S与摄像机的backward一致，AD也是如此。

2. 键盘只能响应WASD中最后一个按下的键，所以要保存当前独占键。通过判断此前保存的持续按键在当前帧是否仍处于激活状态来决定是否需要激活新的持续按键。

   ```c#
   if (!Input.GetKey(pressingKey)) {
               if (Input.GetKey(KeyCode.W)) {
                   pressingKey = KeyCode.W;
               } else if (Input.GetKey(KeyCode.S)) {
                   pressingKey = KeyCode.S;
               } else if (Input.GetKey(KeyCode.A)) {
                   pressingKey = KeyCode.A;
               } else if (Input.GetKey(KeyCode.D)) {
                   pressingKey = KeyCode.D;
               } else {
                   pressingKey = KeyCode.None;
               }
           }
   
           if (Input.GetKeyDown(KeyCode.W)) pressingKey = KeyCode.W;
           if (Input.GetKeyDown(KeyCode.S)) pressingKey = KeyCode.S;
           if (Input.GetKeyDown(KeyCode.A)) pressingKey = KeyCode.A;
           if (Input.GetKeyDown(KeyCode.D)) pressingKey = KeyCode.D;
   ```