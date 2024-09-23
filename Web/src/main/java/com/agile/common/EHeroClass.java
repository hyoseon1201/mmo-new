package com.agile.common;

import lombok.Getter;

@Getter
public enum EHeroClass {

    NONE(0),
    WARRIOR(1),
    RANGER(2),
    WIZARD(3),
    ROGUE(4);

    private final int value;

    EHeroClass(int value) {
        this.value = value;
    }

}
