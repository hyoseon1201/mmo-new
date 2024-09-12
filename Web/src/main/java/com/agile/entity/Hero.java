package com.agile.entity;

import jakarta.persistence.*;
import lombok.Getter;

import java.time.LocalDateTime;

@Entity
@Getter
public class Hero {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "hero_id")
    private long id;

    @Column(name = "account_db_id")
    private long accountDbId;

    @Column(name = "created_at")
    private LocalDateTime createdAt;
}
