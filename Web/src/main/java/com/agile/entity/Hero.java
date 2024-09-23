package com.agile.entity;

import com.agile.common.EHeroClass;
import com.fasterxml.jackson.annotation.JsonBackReference;
import jakarta.persistence.*;
import lombok.Getter;

import java.time.LocalDateTime;

@Entity
@Getter
public class Hero {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "hero_db_id")
    private long heroDbId;

    @Column(name = "nickname", nullable = false)
    private String nickName;

    @Enumerated(EnumType.ORDINAL)
    @Column(name = "class_type", nullable = false)
    private EHeroClass classType = EHeroClass.NONE;

    @Column(name = "level", nullable = false)
    private int level = 1;

    @Column(name = "exp", nullable = false)
    private int exp = 0;

    @Column(name = "map_id", nullable = false)
    private int mapId = 0;

    @Column(name = "pos_x", nullable = false)
    private int posX = 0;

    @Column(name = "pos_y", nullable = false)
    private int posY = 0;

    @Column(name = "created_at", nullable = false)
    private LocalDateTime createdAt;

    @Column(name = "updated_at", nullable = false)
    private LocalDateTime updatedAt;

    @JsonBackReference
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "account_db_id", nullable = false)
    private Account account;
}
