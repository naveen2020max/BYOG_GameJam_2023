﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGun 
{
    float FireRate { get; set; }
    void Shoot();
}
