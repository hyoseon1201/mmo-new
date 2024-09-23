package com.agile.dto;

import org.springframework.security.core.authority.SimpleGrantedAuthority;
import org.springframework.security.core.userdetails.User;

import java.util.*;
import java.util.stream.Collectors;

public class AccountDto extends User {

    private String accountDbId;

    public AccountDto(String accountDbId, String password, boolean isSocial, List<String> roleNames) {
        super(
                accountDbId,
                password,
                roleNames.stream().map(str -> new SimpleGrantedAuthority("ROLE_" + str)).collect(Collectors.toList()));

        this.accountDbId = accountDbId;
    }

    public Map<String, Object> getClaims() {
        Map<String, Object> dataMap = new HashMap<>();

        dataMap.put("accountDbId", accountDbId);
//        dataMap.put("password", password);
//        dataMap.put("isSocial", isSocial);
//        dataMap.put("roleNames", roleNames);

        return dataMap;
    }
}
