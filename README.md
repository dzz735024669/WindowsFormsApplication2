graph TD
    A[开始: validate_qc(Element, Stats, ConfLevel)] --> B{加载原始数据 & 初始化结果字典};

    B --> C{检查：要素名是否有效? (eg. 'TEM')};
    C -- 否 --> Z[结束: 返回 None];
    C -- 是 --> D[数据准备：排序、确定站号列、获取日出时间('06:00:00')];

    D --> E{遍历原始数据中的每一行 (行 N)};

    E --> F{过滤 1: 当前行数据是否有效 (非 NaN / 999999)?};
    F -- 否 --> J(下一行);
    F -- 是 --> G{过滤 2: 计算日出后小时数 H_after};
    
    G -- H_after < 0 或 > 23? --> J;
    G -- H_after 有效 --> H{过滤 3: 获取当前行对应的天气模式 W_mode};

    H --> I{过滤 4: 当前 W_mode 在统计数据 Stats 中是否存在匹配的统计行?};
    I -- 否 --> J;
    I -- 是 --> K[获取统计参数：Mean_H, StdDev_H];
    
    K --> L[计算差值：Diff = Current_Value - Sunrise_Value];
    
    L --> M[计算统计阈值：Lower_Bound = Mean_H - ConfLevel * StdDev_H];
    M --> N[Upper_Bound = Mean_H + ConfLevel * StdDev_H];
    
    N --> O{质控判断：是否正常? (Lower_Bound <= Diff <= Upper_Bound)};

    O -- 是 --> P[算法判定：正常];
    O -- 否 --> Q[算法判定：异常];
    
    P --> R[更新混淆矩阵：TN += 1];
    Q --> S[更新混淆矩阵：FP += 1 (误报)]; 
    
    R --> J;
    S --> J;
    
    J(下一行) --> E;
    J --> T{所有行处理完毕?};
    
    T -- 是 --> U[计算最终准确率：Accuracy = TN / Total_Samples];
    U --> V[组装结果字典 (包含Accuracy, TN, FP, Details)];
    V --> W[结束: 返回结果字典];
    
    subgraph 核心假设 (Core Logic)
        R --> A1[真实标签：所有数据视为 ✔️正常];
        S --> A1;
    end
